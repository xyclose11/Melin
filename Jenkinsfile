pipeline {
	agent any
	stages {
		stage('Restore Packages') {
			sh 'dotnet restore Melin.Server.sln'
		}

		stage('Clean') {
			sh 'dotnet clean Melin.Server.sln --configuration Release'
		}

		stage('Build') {
			steps {
				sh 'dotnet publish --configuration Release --no-restore'
			}
		}
	}
}
