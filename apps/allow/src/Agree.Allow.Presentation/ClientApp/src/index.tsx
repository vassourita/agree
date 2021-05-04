import React from 'react'
import ReactDOM from 'react-dom'
import { App } from './presentation/App'
import reportWebVitals from './reportWebVitals'

import 'bootstrap/dist/css/bootstrap.css'

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
)

reportWebVitals()
