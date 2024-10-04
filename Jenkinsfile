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
		stage('Checkout') {
			steps {
				git credentialsId: 'xyclose11', url: 'https://github.com/xyclose11/Melin.git', branch: 'master'
			}
		}

		stage('Clean Workspace') {
			steps {
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

	}
}
