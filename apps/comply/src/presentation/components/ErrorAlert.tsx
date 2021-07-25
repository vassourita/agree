import Link from 'next/link'
import Router from 'next/router'
import { PropsWithChildren, useContext } from 'react'
import { AuthContext } from '../../logic/contexts/AuthContext'
import { ErrorList } from '../../logic/models/ErrorList'

export function ErrorAlert(props: { errors: ErrorList }) {
  const list = Object.values(props.errors).flat()

  return (
    <div>
      <h6>Errors:</h6>
      <ul>
        {list.map((error, index) => (
          <li key={index}>
            {error}
          </li>
        ))}
      </ul>
    </div>
  )
}