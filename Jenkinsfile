pipeline {
	agent any
	environment {
		DOTNET_BUILD_PATH = "/bin/Release/net8.0/"
		MELIN_SERVER_PATH = "Melin.Server"
	}
	options {
		skipDefaultCheckout(true)
	}
	stages {
		stage('Clean Workspace') {
			steps {
				sh "pwd"
				sh "whoami"

				cleanWs()
		
				echo "Building ${env.JOB_NAME}"
			}
		}

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

		stage('Deploy') {
			steps {
				sh """
					dotnet ${DOTNET_BUILD_PATH}/Melin.Server.dll
				"""
			}
		}
	}
}
