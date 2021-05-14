import * as Yup from 'yup'

import { IValidator } from './IValidator'

export type RegisterInput = {
  email: string
  userName: string
  password: string
  confirmPassword: string
}

export class RegisterValidator implements IValidator<RegisterInput> {
  public async validate (input: RegisterInput): Promise<null | Yup.ValidationError> {
    const schema = Yup.object().shape({
      email: Yup.string().required('Email is required')
        .email('Email must be a valid email address'),
      userName: Yup.string().required('UserName is required')
        .min(1, 'UserName must have at least ${min} characters')
        .max(20, 'UserName must not have more than ${max} characters'),
      password: Yup.string().required('Password is required')
        .min(6, 'Password must have at least ${min} characters')
        .max(255, 'Password must not have more than ${max} characters')
        .matches(
          /^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])[A-Za-z\d]{6,}$/,
          'Password must contain one uppercase, one lowercase and one digit characters'
        ),
      confirmPassword: Yup.string().required('Password confirmation is required')
        .oneOf([Yup.ref('password'), null], 'Passwords must match')
    })

    try {
      await schema.validate(input, { abortEarly: false })
      return null
    } catch (error) {
      if (error instanceof Yup.ValidationError) {
        return error
      }
      return null
    }
  }
}
