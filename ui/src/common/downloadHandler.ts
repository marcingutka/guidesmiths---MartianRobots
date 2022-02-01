import FileDownload  from "js-file-download";
import { GetRobotsResults } from "../services/DataSetApiRequest";

export const downloadHandler = async (runId: string) => {
    const response = await GetRobotsResults(runId);
    const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
    FileDownload(response.data, fileName? fileName : {runId} + ".txt");
  }