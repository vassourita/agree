import { ParseExpireDatePipe } from './parse-expire-date.pipe'

describe('ParseExpireDatePipe', () => {
  let sut: ParseExpireDatePipe

  beforeEach(() => {
    sut = new ParseExpireDatePipe()
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
    expect(sut.transform).toBeDefined()
  })

  it('should return a date on the vercel/ms stardard', async () => {
    expect(await sut.transform('7 days')).toEqual('7d')
    expect(await sut.transform('1 hour')).toEqual('1h')
    expect(await sut.transform('1y')).toEqual('365d')
    expect(await sut.transform('1 year')).toEqual('365d')
  })

  it('should return undefined if a falsy value is passed', async () => {
    expect(await sut.transform(undefined)).toEqual(undefined)
    expect(await sut.transform(null)).toEqual(undefined)
    expect(await sut.transform(false)).toEqual(undefined)
    expect(await sut.transform(0)).toEqual(undefined)
    expect(await sut.transform('')).toEqual(undefined)
  })

  it('should throw if sent date is not falsy and not a string', async () => {
    await expect(() => sut.transform(6000)).rejects.toThrow('Invalid expire date')
    await expect(() => sut.transform(1)).rejects.toThrow('Invalid expire date')
    await expect(() => sut.transform(true)).rejects.toThrow('Invalid expire date')
  })
})
