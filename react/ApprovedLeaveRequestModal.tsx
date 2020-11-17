import React, { useState } from 'react';
import { MdMoreHoriz } from 'react-icons/md';

interface ComponentProps {
  leaveRequestId: string;
}

const ApprovedLeaveRequestModal: React.SFC<ComponentProps> = prop => {
  const [visible, setVisible] = useState(false);
  return (
    <div>
      <button
        key={prop.leaveRequestId}
        className="manage-btn-style"
        disabled={true}
        onClick={() => {
          setVisible(false);
        }}
      >
        {''}
        <MdMoreHoriz style={{ fontSize: '30px' }} />
        {''}
      </button>
    </div>
  );
};

export default ApprovedLeaveRequestModal;
