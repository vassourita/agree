import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './auth/Layout';
import { Home } from './auth/Home';
import { FetchData } from './auth/FetchData';
import { Counter } from './auth/Counter';
import AuthorizeRoute from './auth/AuthorizeRoute';
import ApiAuthorizationRoutes from './auth/ApiAuthorizationRoutes';
import { ApplicationPaths } from './auth/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <AuthorizeRoute path='/fetch-data' component={FetchData} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
