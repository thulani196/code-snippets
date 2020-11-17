/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { LeaveStateType } from '../../../../core/redux/types/LeaveTypes';
import UpdateLeaveRequestModal from '../modals/UpdateLeaveRequestModal';
import ApprovedLeaveRequestModal from '../modals/ApprovedLeaveRequestModal';
import { CustomProgressBar } from './CustomProgressBar';
import MaterialTable from 'material-table';
import { Leave } from '../../../../core/interfaces/Leave';
import moment from 'moment';
import CircularIndeterminate from '../../../../core/pages/CircularIndeterminate';

interface Props {
  employeeLeaveRequests: LeaveStateType;
}

function LeaveRequests(props: Props) {
  const formatLeaveDates = (leaveDays: Leave[]) => {
    var formattedLeaveDays = [];
    for (let index = 0; index < leaveDays.length; index++) {
      var leaveRequest = leaveDays[index];
      var startDate = moment(leaveRequest.startDate).format('LL');
      var endDate = moment(leaveRequest.endDate).format('LL');
      formattedLeaveDays.push({
        startDate: startDate,
        endDate: endDate,
        status: leaveRequest.status,
        userPrincipalName: leaveRequest.userPrincipalName,
        daysRequested: leaveRequest.daysRequested,
        description: leaveRequest.description,
        leaveType: leaveRequest.leaveType,
        id: leaveRequest.id
      });
    }
    return formattedLeaveDays;
  };

  const headerStyle = {
    fontStyle: 'normal',
    fontSize: '16px',
    lineHeight: '24px',
    letterSpacing: '0.01em',
    color: '#A6ACBE',
    opacity: '0.5',
    fontFamily: 'Poppins, sans-serif'
  };

  const rowStyle = {
    fontStyle: 'normal',
    fontSize: '16px',
    lineHeight: '24px',
    letterSpacing: '0.01em',
    color: '#000000',
    fontFamily: 'Poppins, sans-serif'
  };

  return (
    <div className="zigops-leave-table-container">
      <div>
        {props.employeeLeaveRequests.loading ? (
          <div className="text-center">
            <CircularIndeterminate />
          </div>
        ) : (
          <MaterialTable
            columns={[
              {
                title: 'Leave Type',
                field: 'leaveType',
                filtering: false
              },
              {
                title: 'Start Date',
                field: 'startDate',
                filtering: false
              },
              {
                title: 'End Date',
                field: 'endDate',
                filtering: false
              },
              {
                title: 'Status',
                field: 'status',
                filtering: false
              },
              {
                title: 'Manage',
                field: 'action',
                filtering: false,
                sorting: false,
                // eslint-disable-next-line react/display-name
                render: record => (
                  <div>
                    {record.status === 'Approved' ? (
                      <ApprovedLeaveRequestModal leaveRequestId={record.id} />
                    ) : (
                      <UpdateLeaveRequestModal
                        leaveRequestId={record.id}
                        employeeLeaveRequests={props.employeeLeaveRequests}
                      />
                    )}
                  </div>
                )
              }
            ]}
            options={{
              rowStyle,
              headerStyle,
              toolbar: false
            }}
            data={formatLeaveDates(props.employeeLeaveRequests.leaveRequests)}
          />
        )}

        <br />
        {props.employeeLeaveRequests.loading ? (
          <div></div>
        ) : (
          <div className="col-sm-3">
            <CustomProgressBar
              variant="determinate"
              value={
                100 - props.employeeLeaveRequests.daysClaimedThisYear * 4.7
              }
              valueBuffer={100}
            />
            {24 - props.employeeLeaveRequests.daysClaimedThisYear === 1 ? (
              <p>1 Leave day left in the year</p>
            ) : (
              <p>
                {24 - props.employeeLeaveRequests.daysClaimedThisYear} Leave
                days left in the year
              </p>
            )}
          </div>
        )}
      </div>
    </div>
  );
}

export default LeaveRequests;
