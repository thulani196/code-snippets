import React, { useState } from 'react';
import CreateLeaveRequestForm from './CreateLeaveRequestForm';
import {
  Button,
  createStyles,
  Grid,
  makeStyles,
  Theme
} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import { LeaveStateType } from '../../../../core/redux/types/LeaveTypes';

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
    },
    requestLeave: {
      margin: theme.spacing(1),
      backgroundColor: '#6F52ED',
      width: 178,
      height: 37,
      color: '#FFFFFF',
      borderRadius: 5,
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontWeight: 600,
      fontSize: 13
    }
  })
);

interface Props {
  employeeLeaveRequests?: LeaveStateType;
}

function LeaveRequestModal(props: Props) {
  const [open, setOpen] = useState<boolean>(false);
  const classes = useStyles();

  return (
    <div>
      <Grid container>
        <Grid item className="zigops-leave-heading-grid">
          <h4 className="zigops-leave">Leave Days</h4>
        </Grid>
        <Grid item>
          <div className="zigops-request-leave-button-container">
            <Button
              className={classes.requestLeave}
              onClick={() => {
                setOpen(true);
              }}
            >
              <AddIcon /> Request Leave
            </Button>
          </div>
        </Grid>
      </Grid>
      {open ? (
        <CreateLeaveRequestForm
          open={open}
          onCancel={() => {
            setOpen(false);
          }}
          leaveRequests={props.employeeLeaveRequests}
        />
      ) : (
        <div></div>
      )}
    </div>
  );
}

export default LeaveRequestModal;
