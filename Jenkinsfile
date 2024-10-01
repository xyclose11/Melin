pipeline {
	agent any
	environment {
		DOTNET_PATH = "/bin/Release/net8.0/:${env.PATH}"
		MELIN_SERVER_PATH = "Melin.Server"
	} 
	stages {
		stage('Restore Packages') {
			steps {
				sh "dotnet restore ${MELIN_SERVER_PATH}/Melin.Server.sln"
			}
		}

		stage('Clean') {
			steps {
				sh "dotnet clean ${MELIN_SERVER_PATH}/Melin.Server.sln --configuration Release"
			}
		}

		stage('Build') {
			steps {
				sh "dotnet publish ${MELIN_SERVER_PATH} --configuration Release --no-restore"
			}
		}
	}
}
