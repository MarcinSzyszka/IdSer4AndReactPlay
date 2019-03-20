import React, { Component } from 'react';
// import UserManager from './oidc-client'
import { UserManager } from 'oidc-client'
export class Counter extends Component {
  static displayName = Counter.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0, userIsLogged: false };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.login = this.login.bind(this);
    this.logout = this.logout.bind(this);
    this.Mgr = new UserManager(this.config);
    var thisObj = this;
    this.Mgr.getUser().then(function (user) {
      if (user) {
        console.log("User logged in", user.profile);
        thisObj.setState({ userIsLogged: true, user: user })
      }
      else {
        debugger;
        thisObj.Mgr.startSilentRenew();
        // thisObj.Mgr.signinSilent().then(function (user) {
        //   debugger;
        //   if (user) {
        //     console.log("User logged in", user.profile);
        //     thisObj.setState({ userIsLogged: true, user: user });
        //   }
        //   else {
        //     console.log("User not logged in");
        //     thisObj.setState({ userIsLogged: false, user: user });
        //   }
        // });

        //         thisObj.Mgr.querySessionStatus().then(function(a){
        // debugger;
        //         }) 
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
    this.Mgr.signinRedirect({state:'counter'});
  }

  logout() {
    this.Mgr.signoutRedirect();
  }

  config = {
    authority: "https://localhost:44375",
    client_id: "reactappjs",
    redirect_uri: "https://localhost:44381/callback",
    silent_redirect_uri: "https://localhost:44381/callback",
    response_type: "code",
    scope: "openid profile api1",
    post_logout_redirect_uri: "https://localhost:44381/",
    automaticSilentRenew: true
  };

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
