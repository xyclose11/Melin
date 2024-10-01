pipeline {
	agent any
	stages {
		stage('Restore Packages') {
			stepss {
				sh 'dotnet restore Melin.Server.sln'
			}
		}

		stage('Clean') {
			steps {
				sh 'dotnet clean Melin.Server.sln --configuration Release'
			}
		}

		stage('Build') {
			steps {
				sh 'dotnet publish --configuration Release --no-restore'
			}
		}
	}
}
