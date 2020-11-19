import React from 'react';
import {
  Button,
  createStyles,
  Divider,
  Grid,
  makeStyles,
  Paper,
  Theme,
  Typography
} from '@material-ui/core';
import PayrollHistoryTable from '../components/partials/PayrollHistoryTable';
import PayrollCalendar from '../components/partials/PayrollCalendar';
import PayrollChart from '../components/partials/PayrollChart';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      background: '#FFFFFF',
      margin: '20px',
      fontFamily: 'Poppins'
    },
    content: {
      flexGrow: 1,
      padding: theme.spacing(3)
    },
    gridContainer: {
      marginTop: '40px',
      marginRight: '40px',
      marginBottom: 0,
      marginLeft: '40px',
      paddingTop: '10px',
      paddingBottom: '10px',
      paddingRight: '15px',
      paddingLeft: '15px',
      fontFamily: 'Poppins',
      border: '1px solid #DFE1EB',
      boxSizing: 'border-box',
      boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.13)'
    },
    payrollHistoryContainer: {
      margin: '40px',
      paddingTop: '10px',
      paddingRight: '15px',
      paddingBottom: 0,
      paddingLeft: '15px',
      fontFamily: 'Poppins',
      border: '1px solid #DFE1EB',
      boxSizing: 'border-box',
      boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.13)'
    },
    heading: {
      fontFamily: 'Poppins',
      fontWeight: 'bold',
      fontSize: '18px',
      color: '#000'
    },
    text: {
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontWeight: 'normal',
      color: '#333333',
      fontSize: '16px'
    },
    dateRange: {
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontWeight: 'bold',
      fontSize: '15px',
      color: '#192A3E',
      padding: '25px 0 10px 0'
    },
    runButton: {
      background: '#885AF8',
      textTransform: 'none',
      color: '#fff',
      marginBottom: '15px',
      fontWeight: 'bold',
      fontSize: '18px',
      fontFamily: 'Poppins',
      fontStyle: 'normal'
    },
    calendar: {
      paddingTop: '40px',
      paddingBottom: '150px'
    },
    adHoc: {
      paddingBottom: '10px'
    },
    contractor: {
      padding: '20px 0 40px 0'
    },
    subText: {
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontWeight: 'normal',
      fontSize: '13px',
      lineHeight: '24px',
      color: '#818E9B'
    },
    runPayrollText: {
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontSize: '16px',
      lineHeight: '24px',
      letterSpacing: '0.01em',
      color: '#9B51E0'
    },
    payrollHistoryText: {
      fontFamily: 'Poppins',
      fontStyle: 'normal',
      fontSize: '14px',
      lineHeight: '24px',
      textAlign: 'right',
      letterSpacing: '0.01em',
      color: '#9B51E0',
      paddingTop: '5px'
    },
    divider: {
      marginTop: '40px'
    },
    paper: {
      padding: theme.spacing(2),
      textAlign: 'center',
      color: theme.palette.text.secondary,
      margin: 5
    }
  })
);

export default function Dashboard() {
  const classes = useStyles();
  return (
    <div className={classes.root}>
      <Grid container direction="row">
        <Grid item xs={12} sm={8}>
          <Grid item className={classes.gridContainer}>
            <Typography className={classes.heading}>Run Payroll</Typography>
            <Typography className={classes.text}>
              Please run payroll by 4:00pm CAT on Wednesday, September 23rd to
              pay your employees. They’ll receive their funds on Friday,
              September 25th.
            </Typography>
            <Typography className={classes.dateRange}>
              23 August - 23 September, 2020
            </Typography>
            <Button variant="contained" className={classes.runButton}>
              Run Employee Payroll
            </Button>
          </Grid>
          <Grid item className={classes.gridContainer}>
            <PayrollChart />
          </Grid>
          <Grid item className={classes.payrollHistoryContainer}>
            <Typography className={classes.heading}>Payroll History</Typography>
            <PayrollHistoryTable />
            <Typography className={classes.payrollHistoryText}>
              Show all payroll activity
            </Typography>
          </Grid>
        </Grid>
        <Grid item xs={12} sm={4}>
          <Grid item className={classes.calendar}>
            <PayrollCalendar />
          </Grid>
          <Grid item className={classes.adHoc}>
            <Typography className={classes.heading}>Ad Hoc</Typography>
            <Typography className={classes.subText}>
              Run payroll outside your regular pay schedule
            </Typography>
            <Typography className={classes.runPayrollText}>
              Run Ad Hoc Payroll {'>'}
            </Typography>
            <Divider className={classes.divider} />
          </Grid>
          <Grid item className={classes.contractor}>
            <Typography className={classes.heading}>
              Contractor Payroll
            </Typography>
            <Typography className={classes.subText}>Contractor tags</Typography>
            <Typography className={classes.runPayrollText}>
              Run Contractor Payroll {'>'}
            </Typography>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
}
