import { Component } from 'react'
import { Route } from 'react-router'
import { Layout } from './auth/Layout'
import { Home } from './auth/Home'
import { ApiAuthorizationRoutes } from './auth/ApiAuthorizationRoutes'
import { ApplicationPaths } from './auth/ApiAuthorizationConstants'

import './custom.css'
import { BrowserRouter } from 'react-router-dom'

const baseUrl = document.getElementsByTagName('base')?.[0]?.getAttribute('href')

export class App extends Component<any, any> {
  static displayName = App.name

  render () {
    return (
      <BrowserRouter basename={baseUrl || undefined}>
        <Layout>
          <Route exact path='/' component={Home} />
          <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </Layout>
      </BrowserRouter>
    )
  }
}
