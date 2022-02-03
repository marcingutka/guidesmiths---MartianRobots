import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { GridAnalitics } from '../components/Model/GridAnalitics'

const baseApiUrl = basicUrl + "analitics/grid/";

export function getGridAnaliticsData(runId: string): AxiosPromise<GridAnalitics> {
    return axios.get(baseApiUrl + runId);
}