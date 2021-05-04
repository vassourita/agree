import { Component, Fragment } from 'react'
import { Link } from 'react-router-dom'
import { AuthorizeService } from './AuthorizeService'
import { ApplicationPaths } from './ApiAuthorizationConstants'

export class LoginMenu extends Component<any, any> {
    private _subscription!: number

    constructor (props: any) {
      super(props)

      this.state = {
        isAuthenticated: false,
        userName: null
      }
    }

    componentDidMount () {
      this._subscription = AuthorizeService.instance.subscribe(() => this.populateState())
      this.populateState()
    }

    componentWillUnmount () {
      AuthorizeService.instance.unsubscribe(this._subscription)
    }

    async populateState () {
      const [isAuthenticated, user] = await Promise.all([AuthorizeService.instance.isAuthenticated(), AuthorizeService.instance.getUser()])
      this.setState({
        isAuthenticated,
        userName: user && user.name
      })
    }

    render () {
      const { isAuthenticated, userName } = this.state
      if (!isAuthenticated) {
        const registerPath = `${ApplicationPaths.Register}`
        const loginPath = `${ApplicationPaths.Login}`
        return this.anonymousView(registerPath, loginPath)
      } else {
        const profilePath = `${ApplicationPaths.Profile}`
        const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } }
        return this.authenticatedView(userName, profilePath, logoutPath)
      }
    }

    authenticatedView (userName: any, profilePath: any, logoutPath: any) {
      return (<Fragment>
            <div className="nav-item">
                <Link className="nav-link text-dark" to={profilePath}>Hello {userName}</Link>
            </div>
            <div className="nav-item">
                <Link className="nav-link text-dark" to={logoutPath}>Logout</Link>
            </div>
        </Fragment>)
    }

    anonymousView (registerPath: any, loginPath: any) {
      return (<Fragment>
            <div className="nav-item">
                <Link className="nav-link text-dark" to={registerPath}>Register</Link>
            </div>
            <div className="nav-item">
                <Link className="nav-link text-dark" to={loginPath}>Login</Link>
            </div>
        </Fragment>)
    }
}
