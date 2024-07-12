import { Engine } from './engine';

export class SearchRequest {
  public keywords: string = '';
  public url: string = '';
  public engine: Engine = Engine.GOOGLE;
}
