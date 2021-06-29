import { Server } from '../models/Server'

export class ApiResponseToModelMapper {
  public static mapServer (data: any): Server {
    return {
      name: data.name,
      description: data.description,
      members: data.members,
      categories: data.categories,
      roles: data.roles,
      privacy: data.privacy,
      privacyStr: data.privacy_str
    }
  }
}
