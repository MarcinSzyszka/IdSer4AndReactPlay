import React, { Component } from 'react';
import { OidcUserManagerSingletonFactory } from './oidcUserManager'


export class Counter extends Component {
  static displayName = Counter.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0, userIsLogged: false };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.login = this.login.bind(this);
    this.logout = this.logout.bind(this);

    //TODO subscribe callback userLoaded!
    var thisObj = this;
    OidcUserManagerSingletonFactory.GetInstance().getUser().then(function (user) 
    {
      if (user) {
        console.log("User logged in", user.profile);
        thisObj.setState({ userIsLogged: true, user: user })
      }
      else {
        OidcUserManagerSingletonFactory.GetInstance().signinSilent().then(user => {
          OidcUserManagerSingletonFactory.GetInstance().getUser().then(function (user) {
            if (user) {
              console.log("User logged in", user.profile);
              thisObj.setState({ userIsLogged: true, user: user })
            }
          });
        });
        console.log("User not logged in");
        thisObj.setState({ userIsLogged: false, user: user });
      }
    });
  }

  componentDidMount() {
  }

  incrementCounter() {
    if (this.state.userIsLogged) {


      fetch('api/SampleData/WeatherForecasts',
        {
          method: 'get',
          headers: new Headers({
            'Authorization': 'Bearer ' + this.state.user.access_token
          })
        }).then(response => response.json())
        .then(data => {
          console.log(data);
        });

    }
  }

  login() {
    OidcUserManagerSingletonFactory.GetInstance().signinRedirect({ state: 'counter' });
  }

  logout() {
    OidcUserManagerSingletonFactory.GetInstance().signoutRedirect();
  }

  getLoginBtn() {
    if (this.state.userIsLogged) {
      return <button className="btn btn-danger" onClick={this.logout}>Logout</button>
    }
    else {
      return <button className="btn btn-primary" onClick={this.login}>Login</button>
    }
  }

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <p>Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
        <br />
        {this.getLoginBtn()}
      </div>
    );
  }
}
