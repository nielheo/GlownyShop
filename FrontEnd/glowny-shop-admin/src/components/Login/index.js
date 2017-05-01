'use strict';
import React from 'react'
import Paper from 'material-ui/Paper'
import {yellow400} from 'material-ui/styles/colors'
import AppBar from 'material-ui/AppBar'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'
import { setUserToken } from '../Common/Cookies'
import { client } from '../Common/OpenId'

import Oidc from 'oidc-client'

const styles = {
  container: {
    textAlign: 'left',
    paddingTop: 100,
    maxWidth: 400,
    margin: 'auto'
  },
  header: {
    fontSize: '1.3em',
    fontWeight: '600',
  },
  button: {
    marginTop: 12,
    float: 'right',
  }
}


export default class IndexPage extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      email: '',
      password: '',
      buttonClick: false,
    }
  }

  _loginResponseButtonClick = () => {
    var settings = {
        authority: 'http://localhost:51000/',
        client_id: "js",
        //client_secret: 'secret',
        redirect_uri: 'http://localhost:3000/login',
        //post_logout_redirect_uri: 'http://localhost:5000/oidc-client-sample.html',
        response_type: 'token',
        scope: 'openid email roles',

        filterProtocolClaims: true,
        loadUserInfo: true
      }
      var client = new Oidc.OidcClient(settings)
      client.processSigninResponse().then(function(response) {
            //signinResponse = null;
            console.log("signin response", response);
        }).catch(function(err) {
            console.log(err);
        })
  }

  _loginButtonClick = () => {
    this.setState({buttonClick: true})
    if (this.state.email !== '' && this.state.password !== '') {
      var config = {
        authority: "http://localhost:51000",
        client_id: "js",
        //client_secret: 'secret',
        redirect_uri: "http://localhost:3000/callback.html",
        response_type: "token",
        scope:"openid profile api1",
        post_logout_redirect_uri : "http://localhost:3000/index.html",
      };
      var mgr = new Oidc.UserManager(config)
      console.log(mgr)

      var settings = {
        authority: 'http://localhost:51000/',
        client_id: "client",
        //client_secret: 'secret',
        redirect_uri: 'http://localhost:3000/login',
        //post_logout_redirect_uri: 'http://localhost:5000/oidc-client-sample.html',
        response_type: 'token',
        scope: 'openid email roles',

        filterProtocolClaims: true,
        loadUserInfo: true
      }
      var client = new Oidc.OidcClient(settings)
      console.log(client)
      client.createSigninRequest({ state: { bar: 15 }, username: 'daniel' }).then(function(req) {
        console.log("signin request", req, "<a href='" + req.url + "'>go signin</a>")
        
      }).catch(function(err) {
          console.log(err);
      })

      

      mgr.getUser().then(function (user) {
        if (user) {
          console.log("User logged in", user.profile);
        }
        else {
          console.log("User not logged in");
        }
      });

     
    }
  }

  _updateField(name, value) {
    this.setState({[name]: value.target.value})
  }

  render() {
    return (
      <div style={styles.container}>
        <div style={styles.header} >Login</div>
        <TextField
          hintText='email'
          fullWidth={true}
          floatingLabelText='Email'
          errorText={this.state.buttonClick && this.state.email.length === 0 && 'Enter an email' }
          value={this.state.email}
          onChange={this._updateField.bind(this, 'email')}
        />
        <TextField
          hintText='password'
          fullWidth={true}
          floatingLabelText='Password'
          errorText={this.state.buttonClick && this.state.password.length === 0 && 'Enter a password' }
          onChange={this._updateField.bind(this, 'password')}
          value={this.state.password}
          type='password'
        />
        <RaisedButton 
          label='Login'
          fullWidth={true} 
          secondary={true} 
          style={styles.button}
          onClick={this._loginButtonClick} />
        <RaisedButton 
          label='Login Response'
          fullWidth={true} 
          secondary={true} 
          style={styles.button}
          onClick={this._loginResponseButtonClick} />
      </div>

    )
  }
}
