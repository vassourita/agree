import { Switch, Route } from 'react-router-dom'
import { AuthPage } from './pages/account/AuthPage'

export function Routes (): JSX.Element {
  return (
    <Switch>
      <Route exact path={['/login', '/register']}>
        <AuthPage />
      </Route>
    </Switch>
  )
}
