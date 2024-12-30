<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/xyclose11/Melin">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Melin</h3>

  <p align="center">
    Academic Reference Manager and Collaboration Tool
    <br />
    <a href="https://github.com/xyclose11/Melin"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/xyclose11/Melin">View Demo</a>
    ·
    <a href="https://github.com/xyclose11/Melin/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/xyclose11/Melin/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>


[![Product Name Screen Shot][product-screenshot]](https://github.com/xyclose11/Melin)

<!-- ABOUT THE PROJECT -->
## About The Project
<p align="right">(<a href="#readme-top">back to top</a>)</p>
Melin is a suite of different academic related tools to assist in the management of references.
Melin is an open source reference management system that enables collaboration with coworkers, peers, colleagues, and more.


### Disclaimer
_The aim is not to replace any existing reference and/or citation management applications that currently exist. The aim is to
expand the field and to see how far I can take the application in a full-stack setting, with the overall goal to scale the project to production.
All in all Melin is a learning experience at its heart, expect issues, expect messy code, expect different implementation styles._

### Objectives

1. Enable users to manage (Create, Read, Update, Destroy) references and other related academic forms of media
2. Allow users to import and export in a variety of formats including but not limited to: _CSV, JSON, Bib(La)Tex, TXT_
3. Ability for any developer to deploy Melin on their own hardware, with security as a focus out of the box, to act as its own independent service
4. Allow users to collaborate together within Teams, or on a partner basis
5. Allow users to share references, bibliographies, and documents
6. Allow users to collaborate in near real-time on a document
7. Allow users to generate a list of citations (bibliography) or in-text citations from a single or a grouping of references/academic media
8. Users should be able to create and/or join several Teams which will consist of their own private collection of references, groups, and documents


### Built With

- ASP.NET Core Web API (Version 8.0)
- C# (Version 12.0)
- React.js: (Tanstack Router, Tanstack Query, Shadcn)
- PostgreSQL

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started
Melin has the capability to run on both Linux and Windows, as long as you are able to install ASP.NET Core, and a database of your choice.

_Currently, the documentation lacks DB setup since there are a vast amount of tutorials that explain each setup step in more detail than I could.
For the DB setup follow any tutorial on how to install PostgreSQL (Windows, or Linux)_


To get a local copy up and running follow these steps.

### Prerequisites

- ASP.NET Core version 8.0 (Windows/Linux)
- C# version 12.0 (Although any version after 10.0 should work fine)
- PostgreSQL server (Windows/Linux) _or your own desired database, but you will have to install a different package for entity framework core see more details in the documentation under section **Using a different Database provider**_

### Client Setup
1. When you first pull the repository traverse into the 'melin.client' project and run the following:
  ```sh
  npm install
  ```
This will install of the client related dependencies that are needed for the application to run

2. Inside of _/melin.client_ Create an environment variable file called `.env.production`:
```sh
touch .env.production
```
- Enter the newly created environment file and remove everything after the '=' and replace with your fully qualified domain name. **WITH QUOTATION MARKS**
```SH
MELIN_SERVER_ADDR= "https://example.com"
```
The clientside is now good to go. You can test this by running:
```SH
npm run dev

OR

npm run preview

OR

npm run start
```
If any errors occur please open an issue on GitHub: https://github.com/xyclose11/Melin/issues 
And I will remediate the documentation as soon as I can.

### Server Setup
1. Enter into the _Melin.Server_ directory.
```SH
cd Melin.Server
```

2. Install dependencies
```SH
dotnet restore
```
3. Create environment variable files:
- Using both the _template.appsettings.json_ & the _template.appsettings.development.json_ as a template create
2 new files called: _appsettings.json_ & _appsettings.development.json_

#### appsettings.json: This will define settings to be used in a production setting
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "ConnectionStrings": {
    "MelinDatabase": "Host=;Username=;Password=;Database=;"
  },
  "SingleUsePasswords": {
    "SINGLE_USE_ADMIN_PASSWORD": "enterDefaultPasswordHere"
  }
}
```

NOTE: For both json files, for the ConnectionString -> "MelinDatabase" For the fields **DO NOT** include any quotation marks
i.e. "MelinDatabase": "Host=localhost;Username=johnDoe;Password=john123;Database=johnDoeDB;"
#### appsettings.Development.json: This will define settings to be used during development
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "ConnectionStrings": {
    "MelinDatabase": "Host=;Username=;Password=;Database=;"
  },
  "SingleUsePasswords": {
    "SINGLE_USE_ADMIN_PASSWORD": "enterDefaultPasswordHere"
  }
}
```
##### Explanation
- SingleUsePasswords: SINGLE_USE_ADMIN_PASSWORD -> This is set so that on the first run of the backend server the database,
will be 'seeded' with an Admin level account. I chose to do it this way to avoid manually creating an Admin level account in the database.
As the name suggests, I strongly urge you to use a single use password for the initial account creation, and then change it afterward to something more secure.

4. Database Setup
- As previously stated I will not go through the installation of the PostgreSQL instance itself as there are plenty of well-rounded tutorials already out there.
- However once the database is up and running, and you have implemented the connection string in both appsettings in the previous step, you will need to
run the DB migrations to create the tables.
- **Important**: There are 2 main DB contexts within Melin: "ReferenceContext" & "DataContext". ReferenceContext handles everything related to the business logic of Melin.
DataContext deals with anything related to the Users and how they interact with Teams.

With this said you will have to specify the DB context you are attempting to migrate

```sh
dotnet ef database update --context "ReferenceContext"

THEN

dotnet ef database update --context "DataContext"
```

**NOTE: I AM ASSUMING THAT THIS STEP WILL CAUSE THE MOST PROBLEMS ON THE INITIAL SETUP AS THE MIGRATION HISTORY IS QUITE MESSY
THIS WILL BE FIXED IN THE NEXT VERSION OF MELIN**

5. Running the Backend

#### Development
In a development environment use the following command:
```SH
dotnet run
```

### Production Build

**NOTE:** For a production build you do not have to worry about build _melin.client_ since the backend .csproj will handle that
```SH
dotnet publish -c Release

OR (you can specify your own output directory with --output)

dotnet publish -c Release --output /put/output/here


Production RUN (default file location)
** Ensure that you are in the Melin.Server directory

dotnet /bin/Release/net8.0/Melin.Server.dll
```



<!-- ROADMAP -->
## Roadmap
Current abstracted roadmap for the remainder of 2024

- [ ] Import Reference(s) from (JSON, CSL-JSON, CSV, TXT, BibTex, BibLaTex, ISBN API, ISSN API, etc.)
- [ ] Export Reference(s) in (JSON, CSV, TXT, BibTex, BibLaTex, CSL-JSON)
- [ ] Barcode Scanner for mobile devices

See the [open issues](https://github.com/xyclose11/Melin/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Top contributors:

<a href="https://github.com/xyclose11/Melin/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=xyclose11/Melin" alt="contrib.rocks image" />
</a>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Project Link: [https://github.com/xyclose11/Melin](https://github.com/xyclose11/Melin)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/xyclose11/Melin.svg?style=for-the-badge
[contributors-url]: https://github.com/xyclose11/Melin/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/xyclose11/Melin.svg?style=for-the-badge
[forks-url]: https://github.com/xyclose11/Melin/network/members
[stars-shield]: https://img.shields.io/github/stars/xyclose11/Melin.svg?style=for-the-badge
[stars-url]: https://github.com/xyclose11/Melin/stargazers
[issues-shield]: https://img.shields.io/github/issues/xyclose11/Melin.svg?style=for-the-badge
[issues-url]: https://github.com/xyclose11/Melin/issues
[license-shield]: https://img.shields.io/github/license/xyclose11/Melin.svg?style=for-the-badge
[license-url]: https://github.com/xyclose11/Melin/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/library-page.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
