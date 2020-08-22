import { CreateUserUseCase } from './create-user/create-user.use-case'
import { FindUserByEmailUseCase } from './find-user-by-email/find-user-by-email.use-case'
import { FindUserByIdUseCase } from './find-user-by-id/find-user-by-id.use-case'
import { FindUserByNameAndTagUseCase } from './find-user-by-name-and-tag/find-user-by-name-and-tag.use-case'
import { ListUsersUseCase } from './list-users/list-users.use-case'
import { UpdateUserUseCase } from './update-user/update-user.use-case'
import { UploadAvatarUseCase } from './upload-avatar/upload-avatar.use-case'

export const useCases = [
  ListUsersUseCase,
  CreateUserUseCase,
  UpdateUserUseCase,
  UploadAvatarUseCase,
  FindUserByIdUseCase,
  FindUserByEmailUseCase,
  FindUserByNameAndTagUseCase
]
