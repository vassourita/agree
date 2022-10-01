import { ValidationError } from 'yup'

export interface IValidator<T> {
  validate(input: T): Promise<null | ValidationError>
}
