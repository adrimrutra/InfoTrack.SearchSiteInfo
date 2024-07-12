export class BaseReponse<T> {
  public succcess?: boolean;
  public data?: T;
  public message?: string;
  public errors?: [];
}
