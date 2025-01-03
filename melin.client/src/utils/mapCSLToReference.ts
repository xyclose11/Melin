import { CSLJSON, DateVariable } from "@/utils/CSLJSON.ts";
import { IReference } from "@/utils/Reference.ts";

export function mapCSLToReference(csl: CSLJSON) {
    const type = csl.type.toLowerCase();
    if (!type) {
        throw new Error("Type is required when mapping CSL to Reference");
    }

    const convertedData: IReference = {
        URL: csl.URL,
        abstractNote: csl.abstract,
        accessed: parseDate(csl.accessed),
        applicationNumber: "",
        archive: "",
        archiveLocation: "",
        assignee: "",
        audioRecordingFormat: "",
        billNumber: "",
        blogTitle: "",
        bookTitle: "",
        callNumber: "",
        caseName: "",
        citationKey: "",
        code: "",
        codePages: "",
        codeVolume: "",
        committee: "",
        company: "",
        conferenceName: "",
        country: "",
        court: "",
        creators: [],
        dataType: "",
        date: "",
        dateDecided: "",
        datePublished: undefined,
        dimensions: "",
        distributor: "",
        docketNumber: "",
        documentNumber: "",
        doi: "",
        edition: "",
        episodeNumber: "",
        extraFields: [],
        fileFormat: "",
        filingDate: "",
        firstPage: "",
        format: "",
        genre: "",
        history: "",
        identifier: "",
        isbn: "",
        issn: "",
        issue: "",
        issueDate: "",
        issuingAuthority: "",
        journalAbbr: "",
        label: "",
        language: "",
        legalStatus: "",
        legislativeBody: "",
        libraryCatalog: "",
        locationStored: "",
        manuscriptType: "",
        mapType: "",
        medium: "",
        numberOfPages: "",
        numberOfVolumes: 0,
        pageAmount: 0,
        pages: "",
        patentNumber: "",
        place: "",
        postType: "",
        priorityNumber: "",
        proceedingsTitle: "",
        programTitle: "",
        programmingLanguage: "",
        publication: "",
        publisher: "",
        references: [],
        reporter: "",
        reporterVolume: "",
        repository: "",
        repositoryLocation: "",
        rights: [],
        runningTime: "",
        scale: "",
        section: "",
        series: "",
        seriesNumber: 0,
        seriesTitle: "",
        session: "",
        subject: "",
        system: "",
        versionNumber: "",
        videoRecordingFormat: "",
        volume: "",
        volumeAmount: 0,
        websiteType: "",
        type: csl.type,
        title: csl.title ?? "",
    };

    return convertedData;
}

function mapAuthors(authors: CSLJSON[]): Author[] {
    if (!authors) return [];
    return authors.map((author) => ({
        firstName: author.given || "",
        lastName: author.family || "",
    }));
}
function parsePages(page: string): number | undefined {
    const pageNumber = parseInt(page, 10);
    return isNaN(pageNumber) ? undefined : pageNumber;
}

// Following EDTF Level 0 standard
// Requires that YYYY always be present
export function parseDate(date: DateVariable): string | undefined {
    if (!date) {
        return undefined;
    }
    let year, month, day;

    if (
        date["date-parts"] !== undefined &&
        date["date-parts"][0] !== undefined
    ) {
        const d = date["date-parts"][0];
        year = d[0];
        month = d[1];
        day = d[2];
    }

    if (year === undefined) {
        return undefined;
    }

    if (month === undefined) {
        return year.toString();
    }

    if (parseInt(month.toString()) > 12 || parseInt(month.toString()) < 1) {
        return undefined;
    }

    if (day === undefined) {
        return year + "-" + month;
    }

    if (parseInt(day.toString()) > 31 || parseInt(day.toString()) < 1) {
        return undefined;
    }

    return year + "-" + month + "-" + day;
}
