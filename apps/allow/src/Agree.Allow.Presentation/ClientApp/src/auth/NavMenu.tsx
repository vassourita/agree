import { Component } from 'react'
import { Link } from 'react-router-dom'
import { LoginMenu } from './LoginMenu'
import './NavMenu.css';

export class NavMenu extends Component<any, any> {
  static displayName = NavMenu.name;

  constructor (props: any) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 navbar-light">
          <div className="container">
            <Link className="navbar-brand" to="/">Agree.Allow.Presentation</Link >
            <div onClick={this.toggleNavbar} className="mr-2 navbar-toggler" />
            <div className={`d-sm-inline-flex flex-sm-row-reverse collapse ${!this.state.collapsed && 'open'}`}>
              <ul className="navbar-nav flex-grow">
                <div className="nav-item">
                  <Link className="text-dark nav-link" to="/">Home</Link>
                </div>
                <div className="nav-item">
                  <Link className="text-dark nav-link" to="/counter">Counter</Link>
                </div>
                <div className="nav-item">
                  <Link className="text-dark nav-link" to="/fetch-data">Fetch data</Link>
                </div>
                <LoginMenu>
                </LoginMenu>
              </ul>
            </div>
          </div>
        </nav>
      </header>
    );
  }
}
