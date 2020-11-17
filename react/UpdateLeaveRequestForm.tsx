import React, { useEffect, useState } from 'react';
import { AppState } from '../../../../core/redux/store/Index';
import {
  getLeaveRequestById,
  deleteLeaveRequest,
  updateLeaveRequest,
  getAllLeaveRequests
} from '../../../../core/redux/thunk/LeaveRequestEffects';

import moment from 'moment';
import {
  createStyles,
  Fade,
  FormControl,
  Grid,
  InputLabel,
  makeStyles,
  Select,
  TextField,
  Theme
} from '@material-ui/core';
import Modal from '@material-ui/core/Modal';
import Backdrop from '@material-ui/core/Backdrop';
import CircularIndeterminate from '../../../../core/pages/CircularIndeterminate';
import { connect, useSelector } from 'react-redux';
import { Response } from '../../../../core/interfaces/Response';
import { Leave } from '../../../../core/interfaces/Leave';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import Snackbar from '../../../employee/components/snackbars/Snackbar';
import { LeaveStateType } from '../../../../core/redux/types/LeaveTypes';
import { betweenDate } from '../../../../core/utilities/Helpers';
import { DatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { useToasts } from '../../../../utils/context/ToastManager';

interface Props {
  open: boolean;
  onCancel: () => void;
  leaveRequestId: string;
}

interface LeaveApprovalResponse {
  success: boolean;
  message: string | null;
  data: any;
}

const initialLeaveState: Response = {
  success: false,
  message: '',
  data: {} as Leave
};

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
      minWidth: '100%'
    },
    margin: {
      margin: theme.spacing(1)
    }
  })
);

function UpdateLeaveRequestForm(props: any) {
  const [leaveRequest, setLeaveRequest] = useState(initialLeaveState);
  const [leaveType, setLeaveType] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [startDate, setStartDate] = useState<any>('');
  const [endDate, setEndDate] = useState<any>('');
  const [errorMessage, setErrorMessage] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(true);
  const [isUpdating, setIsUpdating] = useState<boolean>(true);
  const [isUpdated, setIsUpdated] = useState<boolean>(false);
  const [isDeletedError, setIsDeletedError] = useState<boolean>(false);
  const [isUpdatedError, setIsUpdatedError] = useState<boolean>(false);
  const [isDeleted, setIsDeleted] = useState<boolean>(false);
  const [leaveTypeError, setLeaveTypeError] = useState<string>('');
  const [descriptionError, setDescriptionError] = useState<string>('');
  const [startDateError, setStartDateError] = useState<string>('');
  const [endDateError, setEndDateError] = useState<string>('');
  const [isOpen, setIsOpen] = useState<boolean>(props.open);
  const classes = useStyles();
  const auth = useSelector((state: AppState) => state.auth);
  const { add } = useToasts();

  useEffect(() => {
    const fetchLeaveRequest = async () => {
      setLeaveRequest(
        await props.getLeaveRequestById(props.leaveRequestId, auth.accessToken)
      );
      setLoading(false);
    };

    const setDefaults = (data: Leave) => {
      setStartDate(data.startDate);
      setEndDate(data.endDate);
      setLeaveType(data.leaveType);
      setDescription(data.description);
    };

    fetchLeaveRequest();
    setDefaults(leaveRequest.data);

    setIsUpdating(false);
    setIsUpdated(false);
    setIsUpdatedError(false);
    setIsDeleted(false);
    setIsDeletedError(false);
  }, [loading]);

  const handleDescription = (e: any) => {
    setDescription(e.target.value);
    setDescriptionError('');
  };

  const onStartDateChange = (e: any) => {
    const dateString = moment(e).format();
    setStartDate(dateString);
    setStartDateError('');
  };

  const onEndDateChange = (date: any) => {
    const dateString = moment(date).format();
    setEndDate(dateString);
    setEndDateError('');
  };

  const onLeaveTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setLeaveType(event.target.value as string);
    setLeaveTypeError('');
  };

  const onDelete = async () => {
    setIsUpdating(true);
    var response = (await props.deleteLeaveRequest(
      props.leaveRequestId,
      auth.accessToken
    )) as LeaveApprovalResponse;
    setIsUpdating(false);

    if (response.success) {
      add('success', 'Leave Request Deleted.');
      setIsDeleted(true);
    } else {
      setErrorMessage(response.message);
      add('error', 'Leave Request Deleted.');
      setIsDeletedError(true);
    }
    await props.getAllLeaveRequests(auth.accessToken);
    props.onCancel();
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
    let descriptionError = '';
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
    if (description === '') {
      descriptionError = 'Description cannot be empty';
      setDescriptionError(descriptionError);
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

  const onSubmit = async (e: any) => {
    e.preventDefault();
    const isValid = validate();
    if (isValid) {
      setIsUpdating(true);

      const data = {
        startDate: startDate,
        endDate: endDate,
        leaveType: leaveType,
        description: description
      };

      var response = (await props.updateLeaveRequest(
        props.leaveRequestId,
        data,
        auth.accessToken
      )) as LeaveApprovalResponse;

      setIsUpdating(false);
      if (response.success) {
        add('success', 'Leave Request Updated.');
        setIsUpdated(true);
      } else {
        setErrorMessage(JSON.stringify(response));
        add('error', JSON.stringify(response));
        setIsUpdatedError(true);
      }
      await props.getAllLeaveRequests(auth.accessToken);
      props.onCancel();
    }
  };

  const disableCustomDate = (current: any) => {
    let leaveRequests = props.employeeLeaveRequests as LeaveStateType;
    var disabledDates: any[] = [];

    if (Object.keys(leaveRequests).length != 0) {
      if (leaveRequests.leaveRequests.length > 0) {
        leaveRequests.leaveRequests.forEach(leave => {
          if (leave.status === 'Approved') {
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

  const leaveData = leaveRequest.data as Leave;

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
            {loading ? (
              <div className="text-center">
                <CircularIndeterminate />
              </div>
            ) : (
              <form onSubmit={onSubmit}>
                <p className="zigops-leave-request-form-heading">
                  Edit Leave Request
                </p>
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
                  <Grid item xs={12} md={12} lg={12} xl={12}>
                    <FormControl
                      variant="outlined"
                      className={classes.formControl}
                    >
                      <InputLabel>Leave Type</InputLabel>
                      <Select
                        defaultValue={leaveData.leaveType}
                        onChange={onLeaveTypeChange}
                        inputProps={{
                          name: 'Leave Type',
                          id: 'filled-age-native-simple'
                        }}
                        label="Leave Type"
                      >
                        <option value="Annual">Annual Leave</option>
                        <option value="Maternity">Maternity Leave</option>
                        <option value="Paternity">Paternity Leave</option>
                        <option value="Sick">Sick Leave</option>
                      </Select>
                    </FormControl>
                    <p className="validate-error-styles">{leaveTypeError}</p>
                  </Grid>
                </Grid>
                <Grid container>
                  <Grid item xs={12} md={12} lg={12} xl={12}>
                    <FormControl>
                      <TextField
                        id="outlined-multiline-static"
                        label="Description"
                        multiline
                        inputProps={{}}
                        onChange={handleDescription}
                        defaultValue={leaveData.description}
                        rows={6}
                        variant="outlined"
                        className="zigops-text-area"
                      />
                      <p className="validate-error-styles">
                        {descriptionError}
                      </p>
                    </FormControl>
                  </Grid>
                </Grid>
                {loading ? (
                  <Grid container>
                    <Grid container className="text-center">
                      <CircularIndeterminate />
                    </Grid>
                  </Grid>
                ) : (
                  <div>
                    {isUpdating ? (
                      <div className="zigops-center-item">
                        <CircularIndeterminate />
                      </div>
                    ) : (
                      <div>
                        {isDeleted ||
                        isUpdated ||
                        isDeletedError ||
                        isUpdatedError ? (
                          <div className="zigops-submit-leave-request"></div>
                        ) : (
                          <Grid container>
                            <Grid item xl={6} lg={6} md={12} xs={12} sm={12}>
                              <div>
                                <input
                                  type="submit"
                                  value="Update Request"
                                  className="zigops-submit-leave-request-button"
                                />
                              </div>
                            </Grid>
                            <Grid item xl={6} lg={6} md={12} xs={12} sm={12}>
                              <div>
                                <button
                                  type="button"
                                  onClick={onDelete}
                                  className="zigops-delete-leave-request-button"
                                >
                                  Delete Request
                                </button>
                              </div>
                            </Grid>
                          </Grid>
                        )}
                      </div>
                    )}
                  </div>
                )}
              </form>
            )}
          </div>
        </Fade>
      </Modal>
    </MuiPickersUtilsProvider>
  );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
  bindActionCreators(
    {
      updateLeaveRequest,
      getLeaveRequestById,
      deleteLeaveRequest,
      getAllLeaveRequests
    },
    dispatch
  );

export default connect(null, mapDispatchToProps)(UpdateLeaveRequestForm);
