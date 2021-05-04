import { Component } from 'react'
import { NavMenu } from './NavMenu'

export class Layout extends Component<any, any> {
  static displayName = Layout.name

  render () {
    return (
      <div>
        <NavMenu />
        <div className="container">
          {this.props.children}
        </div>
      </div>
    )
  }
}
