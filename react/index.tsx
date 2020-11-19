import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './core/styles/main.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@devexpress/dx-react-chart-bootstrap4/dist/dx-react-chart-bootstrap4.css';
import axios, { AxiosRequestConfig } from 'axios';
import * as serviceWorker from './serviceWorker';
import { AUTH_TOKEN_KEY } from './utils/constants';

axios.defaults.baseURL = 'http://localhost:8000';

axios.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    config.headers['Authorization'] = localStorage.getItem(AUTH_TOKEN_KEY);
    config.headers['x-functions-key'] =
      process.env.REACT_APP_ADMIN_AZURE_FUNCTION_KEY;

    return config;
  },
  error => {
    // eslint-disable-next-line no-undef
    return new Promise<string>(reject => reject(error));
  }
);

ReactDOM.render(<App />, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
