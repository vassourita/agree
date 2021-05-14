import { Switch, Route, Redirect } from 'react-router-dom'
import { useAllow } from '../logic/hooks/useAllow'
import { DashboardLayout } from './layouts/DashboardLayout'
import { AuthPage } from './pages/account/AuthPage'
import { HomePage } from './pages/dashboard/HomePage'
import { NotFoundPage } from './pages/error/NotFoundPage'

export function Routes (): JSX.Element {
  const { AuthenticatedTemplate, UnauthenticatedTemplate } = useAllow()

  return (
    <>
      <AuthenticatedTemplate>
        <Switch>
          <Route exact path="/home">
            <DashboardLayout>
              <HomePage />
            </DashboardLayout>
          </Route>
          <Route exact path="/settings">
            <DashboardLayout>
              <HomePage />
            </DashboardLayout>
          </Route>
          <Route exact path="/s/new">
            <DashboardLayout>
              <HomePage />
            </DashboardLayout>
          </Route>
          <Route exact path="/s/search">
            <DashboardLayout>
              <HomePage />
            </DashboardLayout>
          </Route>
          <Route exact path="/s/:id">
            <DashboardLayout>
              <HomePage />
            </DashboardLayout>
          </Route>
          <Route path="*">
            <NotFoundPage />
          </Route>
        </Switch>
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <Switch>
          <Route exact path={['/login', '/register']}>
            <AuthPage />
          </Route>
          <Route path="*">
            <Redirect to="/login" />
          </Route>
        </Switch>
      </UnauthenticatedTemplate>
    </>
  )
}
