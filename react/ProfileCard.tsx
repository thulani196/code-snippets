/* eslint-disable @typescript-eslint/no-use-before-define */
import React, { useState } from 'react';
import { connect } from 'react-redux';
import { AppState } from '../../../../core/redux/store/Index';
import { AuthStateType } from '../../../../core/redux/types/AuthTypes';
import Moment from 'moment';

import {
  Grid,
  makeStyles,
  Avatar,
  Typography,
  Divider,
  Button,
  Box
} from '@material-ui/core';
import { Redirect } from 'react-router-dom';
import { FiEye, FiEyeOff } from 'react-icons/fi';

const useStyles = makeStyles({
  profileAvatar: {
    height: 150,
    width: 150,
    margin: 10
  },
  mainContainer: {
    backgroundColor: '#ffffff',
    borderRadius: 10,
    marginRight: 30,
    padding: 20,
    height: '100%',
    overflow: 'hidden'
  },
  jobDescStyle: {
    fontWeight: 600,
    marginTop: 10,
    marginBottom: 10
  },
  updateButton: {
    backgroundColor: '#6F52ED',
    color: '#fff',
    textTransform: 'none',
    width: 200
  },
  buttonHolder: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'flex-end',
    alignContent: 'flex-end',
    width: '100%'
  },
  dividerPadding: {
    marginBottom: 10,
    marginTop: 10,
    padding: 0.5
  },
  headerStyle: {
    fontWeight: 600,
    marginTop: 10
  },
  payrollHeader: {
    fontWeight: 600,
    marginTop: 50
  },

  titleStyle: {
    marginBottom: 10,
    color: '#6575A4'
  },

  textStyle: {
    marginBottom: 10,
    color: '#000'
  },
  avatarHolder: {
    display: 'flex',
    alignContent: 'center',
    justifyContent: 'center'
  },
  updateButtonHolder: {
    width: '100%',
    display: 'flex',
    justifyContent: 'flex-end',
    alignContent: 'flex-end',
    marginTop: 10
  }
});

function ProfileCard(props: any) {
  const [showAccountNumber, setShowAccountNumber] = useState(false);
  const [update, setUpdate] = useState<boolean>(false);

  const classes = useStyles();

  const handleAccountNumber = () => {
    setShowAccountNumber(!showAccountNumber);
  };
  const handleUpdateProfile = () => {
    setUpdate(true);
  };

  const dateOfBirth = Moment(
    props.authentication.employee.additionalInformation?.dateofBirth
  ).format('LL');
  //This image will need to be included in our cdn
  const defaultProfilePhoto =
    'https://icons-for-free.com/iconfiles/png/512/person+profile+user+icon-1320184051308863170.png';
  const profilePhoto = props.authentication.employee?.profilePhoto;
  const userProfilePhoto = profilePhoto
    ? `data:image/gif;base64,${profilePhoto}`
    : defaultProfilePhoto;
  const dateHired = Moment(
    props.authentication.employee.additionalInformation?.dateHired
  ).format('LL');
  let dateNow = 'January 1, 0001' || Moment(new Date()).format('LL');
  return (
    <div>
      <p className="zigops-profile">PROFILE</p>
      <Box className={classes.mainContainer}>
        <Grid container direction="row" justify="space-evenly" spacing={2}>
          <Grid item xs={12} sm={6} container direction="column">
            <Grid item container justify="center" alignContent="center">
              <Grid
                item
                xs={12}
                container
                justify="center"
                alignContent="center"
              >
                <Avatar
                  className={classes.profileAvatar}
                  src={`data:image/gif;base64,${profilePhoto}`}
                  alt="profile"
                />
              </Grid>
              <Grid
                item
                container
                direction="column"
                justify="space-evenly"
                alignContent="center"
              >
                <Typography variant="h4" className={classes.headerStyle}>
                  {' '}
                  {props.authentication.employee.basicInformation?.displayName}
                </Typography>

                <Typography
                  className={classes.jobDescStyle}
                  variant="subtitle1"
                  align="center"
                >
                  {' '}
                  {props.authentication.employee.basicInformation?.jobTitle}
                </Typography>
                <Typography align="center" variant="subtitle2">
                  {' '}
                  {dateHired === dateNow ? 'DD/MM/YYYY' : dateHired}
                </Typography>
              </Grid>
            </Grid>
            <Grid
              container
              direction="column"
              justify="center"
              alignContent="center"
            >
              <Typography variant="h6" className={classes.payrollHeader}>
                Payroll Information
              </Typography>
              <Divider className={classes.dividerPadding} />
              <Grid item container direction="row" justify="space-between">
                <Grid
                  xs={12}
                  sm={6}
                  item
                  container
                  direction="column"
                  justify="center"
                >
                  <Typography className={classes.titleStyle}>
                    NAPSA SSN
                  </Typography>
                  <Typography className={classes.titleStyle}>TPIN</Typography>
                </Grid>
                <Grid item xs={12} sm={6}>
                  <Typography className={classes.textStyle}> N/A</Typography>
                  <Typography className={classes.textStyle}>N/A</Typography>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={12} sm={6}>
            <Grid item>
              <Typography variant="h6" className={classes.headerStyle}>
                Personal Information
              </Typography>
              <Divider className={classes.dividerPadding} />
              <Grid item container direction="row" justify="center">
                <Grid item xs={12} sm={6} container direction="column">
                  <Typography className={classes.titleStyle}>Gender</Typography>
                  <Typography className={classes.titleStyle}>
                    Date Of Birth
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    NRC Number
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    T-Shirt Size
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    Start Date
                  </Typography>
                </Grid>
                <Grid item xs={12} sm={6}>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.gender || 'N/A'}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {dateOfBirth === dateNow ? 'DD/MM/YYYY' : dateOfBirth}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.nationalRegistrationCardNumber || '000000/00/0'}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.tshirtSize || 'N/A'}
                  </Typography>

                  <Typography className={classes.textStyle}>
                    {dateHired === dateNow ? 'DD/MM/YYYY' : dateHired}
                  </Typography>
                </Grid>
              </Grid>

              <Typography variant="h6" className={classes.headerStyle}>
                Bank Details
              </Typography>
              <Divider className={classes.dividerPadding} />

              <Grid item container direction="row" justify="space-between">
                <Grid
                  xs={12}
                  sm={6}
                  item
                  container
                  direction="column"
                  justify="center"
                >
                  <Typography className={classes.titleStyle}>
                    Account holder Name
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    Bank Name
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    Branch Name
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    Account Number
                  </Typography>
                </Grid>
                <Grid item xs={12} sm={6}>
                  <Typography className={classes.textStyle}>
                    {' '}
                    {props.authentication.employee.additionalInformation
                      ?.accountHolderName || 'N/A'}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.bankName || 'N/A'}
                  </Typography>

                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.bankBranch || 'N/A'}
                  </Typography>
                  <Grid container>
                    <Grid item xs={8}>
                      <Typography className={classes.textStyle}>
                        {showAccountNumber
                          ? props.authentication.employee.additionalInformation
                              ?.accountNumber || 'N/A'
                          : '* * * * * * * * * * * * *'}
                      </Typography>
                    </Grid>
                    <Grid item xs={4}>
                      <span onClick={handleAccountNumber}>
                        {showAccountNumber ? <FiEye /> : <FiEyeOff />}
                      </span>
                    </Grid>
                  </Grid>
                </Grid>
              </Grid>

              <Typography variant="h6" className={classes.headerStyle}>
                Emergency Information
              </Typography>
              <Divider className={classes.dividerPadding} />
              <Grid item container direction="row" justify="space-between">
                <Grid
                  xs={12}
                  sm={6}
                  item
                  container
                  direction="column"
                  justify="center"
                >
                  <Typography className={classes.titleStyle}>
                    Contact Name
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    {' '}
                    Contact Relationship
                  </Typography>
                  <Typography className={classes.titleStyle}>
                    {' '}
                    Contact Phone Number
                  </Typography>
                </Grid>

                <Grid item xs={12} sm={6}>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.emergencyContactName || 'N/A'}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.emergencyContactRelationship || 'N/A'}
                  </Typography>
                  <Typography className={classes.textStyle}>
                    {props.authentication.employee.additionalInformation
                      ?.emergencyContactPhoneNumber || 'N/A'}
                  </Typography>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
        <Box className={classes.updateButtonHolder}>
          <Button
            variant="contained"
            className={classes.updateButton}
            onClick={handleUpdateProfile}
          >
            {' '}
            Update
          </Button>
        </Box>
      </Box>
      {update ? <Redirect to="/employee/profile/update" /> : <></>}
    </div>
  );
}

const mapStateToProps = (state: AppState) => ({
  authentication: state.auth as AuthStateType
});

export default connect(mapStateToProps)(ProfileCard);
