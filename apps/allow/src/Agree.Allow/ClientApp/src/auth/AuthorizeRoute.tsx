import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { ApplicationPaths, QueryParameterNames } from './ApiAuthorizationConstants'
import { AuthorizeService } from './AuthorizeService'

export class AuthorizeRoute extends React.Component<any, any> {
    private _subscription!: number

    constructor (props: any) {
      super(props)

      this.state = {
        ready: false,
        authenticated: false
      }
    }

    componentDidMount () {
      this._subscription = AuthorizeService.instance.subscribe(() => this.authenticationChanged())
      this.populateAuthenticationState()
    }

    componentWillUnmount () {
      AuthorizeService.instance.unsubscribe(this._subscription)
    }

    render () {
      const { ready, authenticated } = this.state
      const link = document.createElement('a')
      link.href = this.props.path
      const returnUrl = `${link.protocol}//${link.host}${link.pathname}${link.search}${link.hash}`
      const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`
      if (!ready) {
        return <div></div>
      } else {
        const { component: Component, ...rest } = this.props
        return <Route {...rest}
                render={(props) => {
                  if (authenticated) {
                    return <Component {...props} />
                  } else {
                    return <Redirect to={redirectUrl} />
                  }
                }} />
      }
    }

    async populateAuthenticationState () {
      const authenticated = await AuthorizeService.instance.isAuthenticated()
      this.setState({ ready: true, authenticated })
    }

    async authenticationChanged () {
      this.setState({ ready: false, authenticated: false })
      await this.populateAuthenticationState()
    }
}
