import { BrowserRouter } from 'react-router-dom'

import { Routes } from './Routes'
import { ContextContainer } from '../logic/contexts/ContextContainer'

import './styles/utils.scss'

export function App (): JSX.Element {
  return (
    <BrowserRouter>
      <ContextContainer>
        <Routes />
      </ContextContainer>
    </BrowserRouter>
  )
}
