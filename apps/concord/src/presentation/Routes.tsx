import { Switch, Route, Redirect } from 'react-router-dom'
import { useAllow } from '../logic/hooks/useAllow'
import { DashboardLayout } from './layouts/DashboardLayout'
import { AuthPage } from './pages/account/AuthPage'
import { HomePage } from './pages/dashboard/HomePage'
import { NotFoundPage } from './pages/error/NotFoundPage'

export function Routes (): JSX.Element {
  const { AuthenticatedRoute, UnauthenticatedRoute } = useAllow()

  return (
    <Switch>
      <DashboardLayout>
      <AuthenticatedRoute exact path="/" component={() => (
        <Redirect to="/home"/>
      )}/>

      <AuthenticatedRoute exact path="/home" component={() => (
        <HomePage />
      )}/>

      <AuthenticatedRoute exact path="/settings" component={() => (
        <HomePage />
      )}/>

      <AuthenticatedRoute exact path="/s/new" component={() => (
        <HomePage />
      )}/>

      <AuthenticatedRoute exact path="/s/search" component={() => (
        <HomePage />
      )}/>

      <AuthenticatedRoute exact path="/s/:id" component={() => (
        <HomePage />
      )}/>
      </DashboardLayout>

      <UnauthenticatedRoute exact path={['/login', '/register']} component={() => (
        <AuthPage />
      )}/>

      <Route path="*">
        <NotFoundPage />
      </Route>
    </Switch>
  )
}
