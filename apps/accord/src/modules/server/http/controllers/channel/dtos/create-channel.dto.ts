import { ApiProperty } from '@nestjs/swagger'

import { ChannelTypes } from '@modules/server/entities/channel.entity'
import { IsEnum, IsNotEmpty, IsString } from 'class-validator'

export class CreateChannelDTO {
  @IsString()
  @IsNotEmpty()
  @ApiProperty({ example: 'welcome to the server' })
  name: string

  @IsString()
  @IsNotEmpty()
  @ApiProperty({ example: 'info' })
  category: string

  @IsString()
  @IsNotEmpty()
  @IsEnum(ChannelTypes)
  @ApiProperty({
    enum: ChannelTypes,
    description: 'type should be "media" for audio/video or "text" for text messages'
  })
  type: ChannelTypes
}
