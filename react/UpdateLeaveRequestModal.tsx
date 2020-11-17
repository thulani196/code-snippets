import React, { useState, FunctionComponent } from 'react';
import UpdateLeaveRequestForm from './UpdateLeaveRequestForm';
import { MdMoreHoriz } from 'react-icons/md';
import { LeaveStateType } from '../../../../core/redux/types/LeaveTypes';

interface ComponentProps {
  leaveRequestId: string;
  employeeLeaveRequests?: LeaveStateType;
}

const UpdateLeaveRequestModal: FunctionComponent<ComponentProps> = prop => {
  const [open, setOpen] = useState<boolean>(false);
  return (
    <div>
      <button
        className="manage-btn-style"
        key={prop.leaveRequestId}
        onClick={() => {
          setOpen(true);
        }}
      >
        <MdMoreHoriz style={{ fontSize: '30px' }} />
      </button>
      {open ? (
        <UpdateLeaveRequestForm
          open={open}
          onCancel={() => {
            setOpen(false);
          }}
          leaveRequestId={prop.leaveRequestId}
          employeeLeaveRequests={prop.employeeLeaveRequests}
        />
      ) : (
        <div></div>
      )}
    </div>
  );
};
export default UpdateLeaveRequestModal;
