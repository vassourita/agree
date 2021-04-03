import { Switch, Route } from 'react-router-dom'
import { LoginPage } from './pages/account/LoginPage'

export function Routes (): JSX.Element {
  return (
    <Switch>
      <Route path={['/login', '/register']}>
        <LoginPage />
      </Route>
    </Switch>
  )
}
