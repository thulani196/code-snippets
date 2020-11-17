/* eslint-disable @typescript-eslint/no-use-before-define */
import React, { useEffect, useState } from 'react';
import { getAllLeaveRequests } from '../../../core/redux/thunk/LeaveRequestEffects';
import LeaveRequests from '../components/partials/LeaveRequests';
import { AppState } from '../../../core/redux/store/Index';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { AuthStateType } from '../../../core/redux/types/AuthTypes';
import { LeaveStateType } from '../../../core/redux/types/LeaveTypes';
import LeaveRequestModal from '../components/modals/LeaveRequestModal';
import SideBarNavigation from '../components/navigation/SideBarNavigation';
import { makeStyles, createStyles, Theme } from '@material-ui/core';
import CircularIndeterminate from '../../../core/pages/CircularIndeterminate';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      paddingTop: '80px'
    },
    content: {
      flexGrow: 1,
      height: '100%',
      padding: theme.spacing(3),
      background: '#DFE1EB'
    }
  })
);

const initialLeaveState = {
  success: false,
  data: {}
};

function ViewLeaveRequests(props: any) {
  const [leaveRequestsResponse, setLeaveRequestsResponse] = useState(
    initialLeaveState
  );

  useEffect(() => {
    const fetchData = async () => {
      setLeaveRequestsResponse(
        await props.getAllLeaveRequests(props.authentication.accessToken)
      );
    };

    fetchData();
  }, [leaveRequestsResponse]);

  const classes = useStyles();

  var leaveRequests = leaveRequestsResponse.data as LeaveStateType;

  return (
    <div className={classes.root}>
      <SideBarNavigation />
      <main className={classes.content}>
        {/*  */}
        {leaveRequestsResponse.success ? (
          <div>
            <LeaveRequestModal employeeLeaveRequests={leaveRequests} />
            <LeaveRequests employeeLeaveRequests={leaveRequests} />
          </div>
        ) : (
          <div>
            <LeaveRequestModal employeeLeaveRequests={leaveRequests} />
            <div className="text-center">
              <CircularIndeterminate />
            </div>
          </div>
        )}
      </main>
    </div>
  );
}

const mapStateToProps = (state: AppState) => ({
  employeeLeaveRequests: state.employeeLeaveRequests as LeaveStateType,
  authentication: state.auth as AuthStateType
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
  bindActionCreators(
    {
      getAllLeaveRequests
    },
    dispatch
  );

export default connect(mapStateToProps, mapDispatchToProps)(ViewLeaveRequests);
