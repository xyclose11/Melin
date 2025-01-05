export interface IGoogleBookAPIResponse {
    id: string;
    volumeInfo: {
        type: string;
        title: string;
        authors: [string];
        publisher: string;
        publishedDate: string;
        pageCount: number;
        language: string;
    };
}
