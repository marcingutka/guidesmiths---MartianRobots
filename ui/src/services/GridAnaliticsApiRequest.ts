import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { IGridAnalitics } from '../components/Model/IGridAnalitics'

const baseApiUrl = basicUrl + "analitics/grid/";

export async function GetGridAnaliticsData(runId: string): Promise<AxiosPromise<IGridAnalitics>> {
    const res = await axios.get(baseApiUrl + runId); 
    return res;
}