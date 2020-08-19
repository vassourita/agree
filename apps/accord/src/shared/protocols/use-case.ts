export interface IUseCase<IRequest, IResponse> {
  execute(data: IRequest): Promise<IResponse>
}
