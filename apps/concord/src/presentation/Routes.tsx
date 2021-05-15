import { Switch, Route, Redirect } from 'react-router-dom'
import { AuthenticatedRoute, UnauthenticatedRoute } from '../logic/hooks/useAllow'
import { DashboardLayout } from './layouts/DashboardLayout'
import { AuthPage } from './pages/account/AuthPage'
import { HomePage } from './pages/dashboard/HomePage'
import { NotFoundPage } from './pages/error/NotFoundPage'

export function Routes (): JSX.Element {
  return (
    <Switch>
      <AuthenticatedRoute exact path={['/', '/home', '/settings', '/s/new', '/s/search', '/s/:id']} component={() => (
        <DashboardLayout>
          <Route render={({ location }) => (
            <Switch location={location}>
              <AuthenticatedRoute exact path="/" component={() => <Redirect to="/home" />}/>
              <AuthenticatedRoute exact path="/home" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/settings" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/s/search" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/s/new" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/s/:id" component={() => <HomePage />}/>
            </Switch>
          )}/>
        </DashboardLayout>
      )}/>

      <UnauthenticatedRoute exact path={['/login', '/register']} component={() => (
        <AuthPage />
      )}/>

      <Route path="*">
        <NotFoundPage />
      </Route>
    </Switch>
  )
}
