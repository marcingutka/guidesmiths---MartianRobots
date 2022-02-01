import FileDownload  from "js-file-download";
import { GetRunResults, GetRunInput } from "../services/DataSetApiRequest";

export const resultsDownloadHandler = async (runId: string) => {
    const response = await GetRunResults(runId);
    const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
    FileDownload(response.data, fileName? fileName : {runId} + "- Result.txt");
}

export const inputDownloadHandler = async (runId: string) => {
    const response = await GetRunInput(runId);
    const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
    FileDownload(response.data, fileName? fileName : {runId} + " - Input.txt");
}