pipeline {
	agent any
	environment {
		DOTNET_PATH = "/bin/Release/net8.0/:${env.PATH}"
	} 
	stages {
		stage('Restore Packages') {
			steps {
				echo "PATH is: ${env.PATH}"
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
