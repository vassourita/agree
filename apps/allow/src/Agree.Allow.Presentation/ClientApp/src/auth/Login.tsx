import { Component } from 'react'
import { AuthenticationResultStatus, AuthorizeService } from './AuthorizeService'
import { LoadingPage } from '../presentation/pages/LoadingPage'
import { LoginActions, QueryParameterNames, ApplicationPaths } from './ApiAuthorizationConstants'

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
export class Login extends Component<any, any> {
  constructor (props: any) {
    super(props)

    this.state = {
      message: undefined
    }
  }

  componentDidMount () {
    const action = this.props.action
    const params = new URLSearchParams(window.location.search)
    const error = params.get(QueryParameterNames.Message)
    switch (action) {
      case LoginActions.Login:
        this.login(this.getReturnUrl())
        break
      case LoginActions.LoginCallback:
        this.processLoginCallback()
        break
      case LoginActions.LoginFailed:
        this.setState({ message: error })
        break
      case LoginActions.Profile:
        this.redirectToProfile()
        break
      case LoginActions.Register:
        this.redirectToRegister()
        break
      default:
        throw new Error(`Invalid action '${action}'`)
    }
  }

  render () {
    const action = this.props.action
    const { message } = this.state

    if (message) {
      return <LoadingPage message={message} />
    } else {
      switch (action) {
        case LoginActions.Login:
          return (<LoadingPage message={'Você será redirecionado para o login em breve'} />)
        case LoginActions.LoginCallback:
          return (<LoadingPage message={'Você será redirecionado para a plataforma em breve'} />)
        case LoginActions.Profile:
        case LoginActions.Register:
          return (<LoadingPage message={'Cadastro'} />)
        default:
          throw new Error(`Invalid action '${action}'`)
      }
    }
  }

  async login (returnUrl: any) {
    const state = { returnUrl }
    const result = await AuthorizeService.instance.signIn(state)
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        break
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(returnUrl)
        break
      case AuthenticationResultStatus.Fail:
        this.setState({ message: (result as any).message })
        break
      default:
        throw new Error(`Invalid status result ${result.status}.`)
    }
  }

  async processLoginCallback () {
    const url = window.location.href
    const result = await AuthorizeService.instance.completeSignIn(url)
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        // There should not be any redirects as the only time completeSignIn finishes
        // is when we are doing a redirect sign in flow.
        throw new Error('Should not redirect.')
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(this.getReturnUrl((result as any).state))
        break
      case AuthenticationResultStatus.Fail:
        this.setState({ message: (result as any).message })
        break
      default:
        throw new Error(`Invalid authentication result status '${result.status}'.`)
    }
  }

  getReturnUrl (state?: any) {
    const params = new URLSearchParams(window.location.search)
    const fromQuery = params.get(QueryParameterNames.ReturnUrl)
    if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.')
    }
    return (state && state.returnUrl) || fromQuery || `${window.location.origin}/`
  }

  redirectToRegister () {
    this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?${QueryParameterNames.ReturnUrl}=${encodeURI(ApplicationPaths.Login)}`)
  }

  redirectToProfile () {
    this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath)
  }

  redirectToApiAuthorizationPath (apiAuthorizationPath: any) {
    const redirectUrl = `${window.location.origin}/${apiAuthorizationPath}`
    // It's important that we do a replace here so that when the user hits the back arrow on the
    // browser they get sent back to where it was on the app instead of to an endpoint on this
    // component.
    window.location.replace(redirectUrl)
  }

  navigateToReturnUrl (returnUrl: any) {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    window.location.replace(returnUrl)
  }
}
