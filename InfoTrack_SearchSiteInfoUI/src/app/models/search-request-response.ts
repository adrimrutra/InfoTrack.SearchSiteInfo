import { Engine } from './engine';

export class SearchRequestResponse {
  public id!: number;
  public keywords!: string;
  public url!: string;
  public engine!: Engine;
  public rank!: number;
  public createdDate!: string;
}
