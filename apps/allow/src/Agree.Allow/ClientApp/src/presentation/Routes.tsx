import { Switch, Route } from 'react-router-dom'
import { ApplicationPaths } from '../auth/ApiAuthorizationConstants'
import { ApiAuthorizationRoutes } from '../auth/ApiAuthorizationRoutes'
import { AuthorizeRoute } from '../auth/AuthorizeRoute'
import { DashboardLayout } from './layouts/DashboardLayout'
import { HomePage } from './pages/dashboard/HomePage'
import { NotFoundPage } from './pages/error/NotFoundPage'

export function Routes (): JSX.Element {
  return (
    <Switch>
      <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      <AuthorizeRoute exact path="/" component={() => (
        <DashboardLayout>
          <HomePage />
        </DashboardLayout>
      )}/>
      <AuthorizeRoute exact path="/settings" component={() => (
        <DashboardLayout>
          <HomePage />
        </DashboardLayout>
      )}/>
      <AuthorizeRoute exact path="/s/new" component={() => (
        <DashboardLayout>
          <HomePage />
        </DashboardLayout>
      )}/>
      <AuthorizeRoute exact path="/s/search" component={() => (
        <DashboardLayout>
          <HomePage />
        </DashboardLayout>
      )}/>
      <AuthorizeRoute exact path="/s/:id" component={() => (
        <DashboardLayout>
          <HomePage />
        </DashboardLayout>
      )}/>
      <AuthorizeRoute path="*" component={() => (
        <NotFoundPage />
      )}/>
    </Switch>
  )
}
