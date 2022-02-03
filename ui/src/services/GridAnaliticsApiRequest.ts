import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { GridAnalitics } from '../components/Model/IGridAnalitics'

const baseApiUrl = basicUrl + "analitics/grid/";

export async function getGridAnaliticsData(runId: string): Promise<AxiosPromise<GridAnalitics>> {
    const res = await axios.get(baseApiUrl + runId); 
    return res;
}