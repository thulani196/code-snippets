import React, { useState } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import Backdrop from '@material-ui/core/Backdrop';
import Fade from '@material-ui/core/Fade';
import { AppState } from '../../../../core/redux/store/Index';
import {
  NewLeaveRequestStateType,
  LeaveStateType
} from '../../../../core/redux/types/LeaveTypes';
import { AuthStateType } from '../../../../core/redux/types/AuthTypes';
import { AnyAction, Dispatch, bindActionCreators } from 'redux';
import {
  createLeaveRequest,
  getAllLeaveRequests
} from '../../../../core/redux/thunk/LeaveRequestEffects';
import { resetCreatedLeaveRequestAction } from '../../../../core/redux/actions/LeaveRequestActions';
import { connect } from 'react-redux';
import {
  Grid,
  FormControl,
  Select,
  InputLabel,
  TextField
} from '@material-ui/core';
import moment from 'moment';
import * as signalR from '@microsoft/signalr';
import CircularIndeterminate from '../../../../core/pages/CircularIndeterminate';
import { betweenDate } from '../../../../core/utilities/Helpers';
import { DatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { useToasts } from '../../../../utils/context/ToastManager';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    modal: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center'
    },
    paper: {
      background: '#ffffff',
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3),
      width: '500px',
      borderRadius: 17
    },
    formControl: {
      minWidth: 430
    },
    margin: {
      margin: theme.spacing(1)
    }
  })
);

const mapStateToProps = (state: AppState) => ({
  auth: state.auth as AuthStateType,
  createdLeaveRequest: state.createdLeaveRequest as NewLeaveRequestStateType,
  employeeLeaveRequests: state.employeeLeaveRequests as LeaveStateType
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
  bindActionCreators(
    {
      createLeaveRequest,
      resetCreatedLeaveRequestAction,
      getAllLeaveRequests
    },
    dispatch
  );

interface LeaveApprovalResponse {
  success: boolean;
  message: string | null;
  data: any;
}

function CreateLeaveRequestForm(props: any) {
  const [startDate, setStartDate] = useState<string>(new Date().toString());
  const [endDate, setEndDate] = useState<string>(new Date().toString());
  const [leaveType, setLeaveType] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [leaveTypeError, setLeaveTypeError] = useState<string>('');
  const [startDateError, setStartDateError] = useState<string>('');
  const [endDateError, setEndDateError] = useState<string>('');
  const [isCreated, setIsCreated] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>('');
  const [isCreatedError, setIsCreatedError] = useState<boolean>(false);
  const [loading, setLoading] = useState(false);
  const [isOpen, setIsOpen] = useState<boolean>(props.open);
  const { add } = useToasts();

  const classes = useStyles();

  const disableCustomDate = (current: any) => {
    let leaveRequests = props.leaveRequests as LeaveStateType;
    var disabledDates: any[] = [];

    if (Object.keys(leaveRequests).length != 0) {
      if (leaveRequests.leaveRequests.length > 0) {
        leaveRequests.leaveRequests.forEach(leave => {
          if (
            leave.status === 'Approved' ||
            leave.status === 'Pending Approval'
          ) {
            var startDate = leave.startDate;
            var endDate = leave.endDate;

            var pickedDates = betweenDate(
              moment(startDate).format('yyyy-MM-DD'),
              moment(endDate).format('yyyy-MM-DD')
            );
            pickedDates.forEach(date => {
              disabledDates.push(date);
            });
          }
        });
      }
    }

    return (
      disabledDates.includes(moment(current).format('YYYY-MM-DD')) ||
      moment(current).day() === 0 ||
      moment(current).day() === 6
    );
  };

  const handleDescription = (description: string) => {
    setDescription(description);
  };

  const onStartDateChange = (date: any) => {
    const dateString = moment(date).format();
    setStartDate(dateString);
    setStartDateError('');
  };

  const onEndDateChange = (date: any) => {
    const dateString = moment(date).format();
    setEndDate(dateString);
    setEndDateError('');
  };

  const onLeaveTypeChange = (value: string) => {
    setLeaveType(value);
    setLeaveTypeError('');
  };

  const daysRequested = () => {
    let date1 = new Date(startDate);
    let date2 = new Date(endDate);
    let differenceInTime = Math.abs(date2.getTime() - date1.getTime());
    let differenceInDays = Math.ceil(differenceInTime / (1000 * 3600 * 24));
    return differenceInDays;
  };

  const validate = () => {
    let leaveTypeError = '';
    let startDateError = '';
    let endDateError = '';
    let currentDate = new Date();
    let currentDateString = moment(currentDate).format();
    const requestedDays = daysRequested();
    let valid = true;

    if (leaveType === '') {
      leaveTypeError = 'Leave type cannot be empty';
      setLeaveTypeError(leaveTypeError);
      valid = false;
    }
    if (startDate === '') {
      startDateError = 'Start date cannot be empty';
      setStartDateError(startDateError);
      valid = false;
    }

    if (startDate < currentDateString) {
      startDateError = 'Start date cannot be earlier than today';
      setStartDateError(startDateError);
      valid = false;
    }
    if (endDate === '') {
      endDateError = 'End date cannot be empty';
      setEndDateError(endDateError);
      valid = false;
    }

    if (startDate > endDate) {
      endDateError = 'End date cannot be earlier than start date';
      setEndDateError(endDateError);
      valid = false;
    }
    if (requestedDays > 24) {
      endDateError = 'Days requested cannot be more than 24';
      setEndDateError(endDateError);
      valid = false;
    }

    return valid;
  };

  const employeeConnectToHub = () => {
    var signalRURL = 'https://zigops-employeement.azurewebsites.net/api';
    var code = '?code';
    var functionKey = process.env.REACT_APP_SIGNALR_NEGOTIATE_FUNCTION_KEY;
    var connection = new signalR.HubConnectionBuilder()
      .withUrl(`${signalRURL}${code}=${functionKey}`)
      .configureLogging(signalR.LogLevel.Information)
      .build();
    connection.start().then(() => {});
    connection.on(props.auth.employee.basicInformation.userPrincipalName, null);
  };

  const onSubmit = async (e: any) => {
    e.preventDefault();
    const isValid = validate();
    if (isValid) {
      setLoading(true);
      const data = {
        startDate,
        endDate,
        leaveType,
        description
      };

      var response = (await props.createLeaveRequest(
        data,
        props.auth?.accessToken
      )) as LeaveApprovalResponse;
      setLoading(false);

      setLeaveType('');
      setDescription('');

      if (response.success) {
        add('success', 'Leave Request Submitted for Approval.');
        setIsCreated(true);
      } else {
        setErrorMessage(JSON.stringify(response));
        add('error', JSON.stringify(response));
        setIsCreatedError(true);
      }

      await props.getAllLeaveRequests(props.auth?.accessToken);
      props.onCancel();
    }
  };

  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils}>
      <Modal
        className={classes.modal}
        open={isOpen}
        onClose={props.onCancel}
        closeAfterTransition
        BackdropComponent={Backdrop}
        BackdropProps={{
          timeout: 500
        }}
      >
        <Fade in={isOpen}>
          <div className={classes.paper}>
            <form onSubmit={onSubmit}>
              <p className="zigops-leave-request-form-heading">Leave Request</p>
              <hr />
              <br />
              <Grid container spacing={8}>
                <Grid item xs={12} md={6} lg={6} xl={6}>
                  <DatePicker
                    inputVariant="outlined"
                    label="End Date"
                    placeholder=""
                    onChange={onStartDateChange}
                    value={startDate}
                    shouldDisableDate={disableCustomDate}
                    format="MM/dd/yyyy"
                  />
                  <p className="validate-error-styles">{startDateError}</p>
                </Grid>
                <Grid item xs={12} md={6} lg={6} xl={6}>
                  <DatePicker
                    inputVariant="outlined"
                    label="End Date"
                    placeholder=""
                    onChange={onEndDateChange}
                    value={endDate}
                    shouldDisableDate={disableCustomDate}
                    format="MM/dd/yyyy"
                  />
                  <p className="validate-error-styles">{endDateError}</p>
                </Grid>
              </Grid>
              <Grid container className="zigops-leave-type-grid">
                <Grid item>
                  <FormControl
                    variant="outlined"
                    className={classes.formControl}
                  >
                    <InputLabel>Leave Type</InputLabel>
                    <Select
                      native
                      defaultValue={leaveType}
                      placeholder="Leave Type"
                      onChange={(e: any) => onLeaveTypeChange(e.target.value)}
                      inputProps={{
                        name: 'Leave Type',
                        id: 'filled-age-native-simple'
                      }}
                      label="Leave Type"
                    >
                      <option aria-label="None" value="" />
                      <option value={1}>Annual Leave</option>
                      <option value={2}>Maternity Leave</option>
                      <option value={8}>Paternity Leave</option>
                      <option value={4}>Sick Leave</option>
                    </Select>
                  </FormControl>
                  <p className="validate-error-styles">{leaveTypeError}</p>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item lg={12} md={12} sm={12} xs={12}>
                  <FormControl>
                    <TextField
                      id="outlined-multiline-static"
                      label="Description"
                      multiline
                      inputProps={{}}
                      onChange={(e: any) => handleDescription(e.target.value)}
                      rows={6}
                      variant="outlined"
                      defaultValue=" "
                      className="zigops-text-area"
                    />
                  </FormControl>
                </Grid>
                <Grid item lg={12} md={12} sm={12} xs={12}>
                  <br />
                  <div className="zigops-center-item">
                    {loading ? (
                      <CircularIndeterminate />
                    ) : (
                      <input
                        type="submit"
                        value="Submit Request"
                        className="zigops-submit-leave-request-button"
                      />
                    )}
                  </div>
                </Grid>
              </Grid>
            </form>
          </div>
        </Fade>
      </Modal>
    </MuiPickersUtilsProvider>
  );
}
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(CreateLeaveRequestForm);
