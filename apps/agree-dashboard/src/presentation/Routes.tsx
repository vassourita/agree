import { Switch, Route, Redirect } from 'react-router-dom'
import { useAuth } from '../logic/hooks/useAuth'
import { AuthPage } from './pages/account/AuthPage'
import { HomePage } from './pages/dashboard/HomePage'

export function Routes (): JSX.Element {
  const { AuthenticatedTemplate, UnauthenticatedTemplate } = useAuth()

  return (
    <>
      <AuthenticatedTemplate>
        <Switch>
          <Route exact path="/">
            <HomePage />
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
