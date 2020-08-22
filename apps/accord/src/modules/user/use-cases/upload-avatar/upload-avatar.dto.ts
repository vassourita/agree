import { IsUUID } from 'class-validator'
import { FileUpload } from 'graphql-upload'

export class UploadAvatarDTO {
  avatarFile?: FileUpload

  @IsUUID('4')
  userId?: string
}
