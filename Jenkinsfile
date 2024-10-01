pipeline {
	agent any
	enviornment {
		DOTNET_PATH = "/bin/Release/net8.0/:${env.PATH}"
	} 
	stages {
		stage('Restore Packages') {
			steps {
				sh 'dotnet restore Melin.Server.sln'
				echo "PATH is: ${env.PATH}"
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
