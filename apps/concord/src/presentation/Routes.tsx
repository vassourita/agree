import { Switch, Route, Redirect } from 'react-router-dom'
import { AuthenticatedRoute, UnauthenticatedRoute } from '../logic/hooks/useAllow'
import { DashboardLayout } from './layouts/DashboardLayout'
import { AuthPage } from './pages/account/AuthPage'
import { HomePage } from './pages/dashboard/HomePage'
import { NotFoundPage } from './pages/error/NotFoundPage'
import { SettingsLayout } from './layouts/SettingsLayout'
import { ServerSearch } from './pages/dashboard/ServerSearch'
import { ServerCreate } from './pages/dashboard/ServerCreate'

export function Routes (): JSX.Element {
  return (
    <Switch>
      <AuthenticatedRoute exact path={[
        '/', '/home', '/settings', '/settings/profile', '/settings/appearance', '/settings/voiceandvideo', '/s/new', '/s/search', '/s/:id', '/u/:id'
      ]} component={() => (
        <DashboardLayout>
          <Route render={({ location }) => (
            <Switch location={location}>
              <AuthenticatedRoute exact path="/" component={() => <Redirect to="/home" />}/>
              <AuthenticatedRoute exact path="/home" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/s/search" component={() => <ServerSearch />}/>
              <AuthenticatedRoute exact path="/s/new" component={() => <ServerCreate />}/>
              <AuthenticatedRoute exact path="/s/:id" component={() => <HomePage />}/>
              <AuthenticatedRoute exact path="/u/:id" component={() => <HomePage />}/>
              <AuthenticatedRoute path="/settings" component={() => <SettingsLayout />}/>
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
