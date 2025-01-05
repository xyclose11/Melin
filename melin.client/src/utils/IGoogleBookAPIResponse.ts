export interface IGoogleBookAPIResponse {
    id: string;
    volumeInfo: {
        type: string;
        title: string;
        authors: [string];
        categories: [string];
        imageLinks: {
            smallThumbnail: string;
        };
        publisher: string;
        publishedDate: string;
        pageCount: number;
        language: string;
        industryIdentifiers: [
            {
                type: string;
                identifier: string;
            },
        ];
    };
}
