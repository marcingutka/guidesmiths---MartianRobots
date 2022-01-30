import axios, {  AxiosPromise } from 'axios';
import { IGridAnalitics } from '../components/Model/IGridAnalitics'

const dataSetApiUrl = "https://localhost:7236/api/analitics/grid/";

export async function GetGridAnaliticsData(runId: string): Promise<AxiosPromise<IGridAnalitics>> {
    const res = await axios.get(dataSetApiUrl + runId); 
    return res;
}