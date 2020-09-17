import { uploadConfig } from './upload.config'

describe('ConfigModule', () => {
  describe('uploadConfig', () => {
    const sut = uploadConfig().filter

    it('should filter files by its extension', async () => {
      const callbackExample = (err, ok) => {
        if (err) throw err
        if (!ok) throw new Error()
        return ok
      }

      expect(sut(null, { originalname: 'image.jpeg' } as any, callbackExample)).toEqual(true)
      expect(sut(null, { originalname: 'image.jpg' } as any, callbackExample)).toEqual(true)
      expect(sut(null, { originalname: 'image.png' } as any, callbackExample)).toEqual(true)
      expect(sut(null, { originalname: 'image.gif' } as any, callbackExample)).toEqual(true)
      expect(() => sut(null, { originalname: 'image.docx' } as any, callbackExample)).toThrow()
      expect(() => sut(null, { originalname: 'image.ts' } as any, callbackExample)).toThrow()
    })
  })
})
